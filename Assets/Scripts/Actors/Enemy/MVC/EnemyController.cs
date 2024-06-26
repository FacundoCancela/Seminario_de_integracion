using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

//Constantes para el blackboard
public static class EnemyBlackBoardConsts
{
    public static string B__IS_DEAD = "bool_IsDead";
}

public class EnemyController : MonoBehaviour
{
    public EnemyStats enemyStats;

    //Variables de IA
    EnemyModel _model;
    EnemyView _view;
    [SerializeField]PlayerController _target;
    EnemyTree _enemyTree;

    ObstacleAvoidance _obstacleAvoidance;
    public float angle;
    public float radius;
    public LayerMask obsMask;

    FSM<EnemyStatesEnum> _fsm;
    ISteering _steering;

    //Variables
    private float shootRange;
    public int actualHealth;

    private Dictionary<string, object> _blackBoardDictionary = new Dictionary<string, object>
    {
        {EnemyBlackBoardConsts.B__IS_DEAD,false },
    };

    private void Awake()
    {
        //referencias
        _model = GetComponent<EnemyModel>();
        _view = GetComponent<EnemyView>();
        _target = FindObjectOfType<PlayerController>();
        
        //inicializaciones 
        InitilizeSteering();
        InitializeFSM();

        //EnemyStats
        shootRange = enemyStats.attackRange;
        actualHealth = enemyStats.maxHealth;

        _enemyTree = new EnemyTree(_fsm, _model.transform, _target.transform, shootRange, ref _blackBoardDictionary);
        _enemyTree.InitializeTree();

    }

    void InitilizeSteering()
    {
        var seek = new Seek(_model.transform, _target.transform);
        
        _steering = seek;
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, obsMask);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<EnemyStatesEnum>();

        //States
        var seek = new EnemyStateSteering<EnemyStatesEnum>(_model,_view, _steering, _obstacleAvoidance);
        var shoot = new EnemyStateShoot<EnemyStatesEnum>(_model, _target.transform, _view);
        var dead = new EnemyStateDead<EnemyStatesEnum>(_model);

        //Transitions

        seek.AddTransition(EnemyStatesEnum.Shoot, shoot);
        seek.AddTransition(EnemyStatesEnum.Dead, dead);

        shoot.AddTransition(EnemyStatesEnum.Seek, seek);
        shoot.AddTransition(EnemyStatesEnum.Dead, dead);

        dead.AddTransition(EnemyStatesEnum.Shoot, shoot);
        dead.AddTransition(EnemyStatesEnum.Seek, seek);

        _fsm.SetInit(seek);
    }


    private void Update()
    {
        _fsm.OnUpdate();
        _enemyTree.ExecuteTree();
    }

    private void DeathCheck()
    {
        if (actualHealth <= 0)
        {
            _blackBoardDictionary[EnemyBlackBoardConsts.B__IS_DEAD] = true;
        }
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
        DeathCheck();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
