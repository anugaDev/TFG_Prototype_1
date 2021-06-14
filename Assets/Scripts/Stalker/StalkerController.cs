using System;
using System.Collections;
using System.Collections.Generic;
using Reliquary.Stalker;
using UniRx;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class StalkerController
    {
        private StalkerModel model;
        private StalkerView view;
        private OnPlayerTouchedCommand _onPlayerTouchedCommand;

        public StalkerController(StalkerModel _model, StalkerView _view, OnPlayerTouchedCommand onPlayerTouchedCommand)
        {
            model = _model;
            view = _view;
            _onPlayerTouchedCommand = onPlayerTouchedCommand;
            view.Controller = this;
            view.SetVisibleDetectionRadius(model.GetDetectionRadius());
            
            //DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG
            view.gameObject.SetActive(true);
            
            model.currentState.AsObservable().Subscribe(newState =>
            {
                view.SetNewState(newState);

                switch (newState)
                {
                    case EEnemyStates.Wander:
                        
                        view.SetCurrentSpeed(model.WanderSpeed);
                        
                        break;
                    case EEnemyStates.Search:
                        
                        view.SetCurrentSpeed(model.PatrolSpeed);
                        view.SetNavMeshDestination(model.GetCurrentTarget().transform);


                        break;
                    case EEnemyStates.Chase:
                        
                        view.SetCurrentSpeed(model.ChaseSpeed);
                        
                        break;
                    case EEnemyStates.Flee:
                        
                        view.SetCurrentSpeed(model.FleeSpeed);
                        view.SetNavMeshDestination(model.GetFleeDestination().transform);

                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
                }

            });
        }

        public void PlayerTouched()
        {
            if (model.currentState.Value == EEnemyStates.Chase)
            {
                _onPlayerTouchedCommand.Execute(model);                
            }
        }

        public void HubTouched()
        {
            model.currentState.Value = EEnemyStates.Flee;
        }

        public void LookArroundFinished()
        {
            model.isLooking.Value = false;
            model.currentState.Value = EEnemyStates.Wander;
        }
        public void Spawn()
        {
            model.currentState.Value = EEnemyStates.Wander;
            view.transform.position = model.GetRandomWanderDestination().position;
            view.StartLoop();
            
        }
        public void StateUpdate()
        {
            Debug.Log("Update State " + model.currentState);
            
            switch (model.currentState.Value)
            {
                case EEnemyStates.Wander:

                    if (view.IsCurrentPathCompleted())
                    {
                        Debug.Log("Set new path");
                        view.SetNavMeshDestination(model.GetRandomWanderDestination());
                    }

                    if (HearingTargetInRange())
                    {
                        model.currentState.Value = EEnemyStates.Search;
                    }
                    break;
                
                case EEnemyStates.Search:

                    if (view.TargetVisible(model.GetCurrentTarget()) && HearingTargetInRange())
                    {
                        model.currentState.Value = EEnemyStates.Chase;
                    }
                    else if(HearingTargetInRange())
                    {
                        view.SetNavMeshDestination(model.GetCurrentTarget().transform);
                    }
                    
                    if (model.isLooking.Value)
                    {                        
                        break;
                    }
                    
                    if (view.IsCurrentPathCompleted())
                    {
                        model.isLooking.Value = true;
                        view.StartCoroutine(view.LookArround());
                    }

                    break;
                case EEnemyStates.Chase:

                    if (model.TargetEscaped(view.transform.position) || !view.TargetVisible(model.GetCurrentTarget()))
                    {
                        model.currentState.Value = EEnemyStates.Search;
                    }
                    
                    view.SetNavMeshDestination(model.GetCurrentTarget().transform);
                    
                    break;
                
                case EEnemyStates.Flee:

                    if (view.IsCurrentPathCompleted())
                    {
                        model.currentState.Value = EEnemyStates.Wander;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool HearingTargetInRange()
        {
            if (model.IsTargetDead())
            {
                return false;
            }
            
            var _vector = model.GetCurrentTarget().transform.position - view.transform.position ;
            _vector.y = 0;
            
            return (_vector.magnitude <= model.GetDetectionRadius()) && model.HearingTarget();
        }
    }
}
