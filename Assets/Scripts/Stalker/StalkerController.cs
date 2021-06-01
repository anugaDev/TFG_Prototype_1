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
        private OnPlayerTouched onPlayerTouched;

        public StalkerController(StalkerModel _model, StalkerView _view, OnPlayerTouched _onPlayerTouched)
        {
            model = _model;
            view = _view;
            onPlayerTouched = _onPlayerTouched;
            view.Controller = this;

            model.currentState.AsObservable().Subscribe(newState =>
            {
                switch (newState)
                {
                    case EStalkerStates.Wander:
                        
                        view.SetCurrentSpeed(model.WanderSpeed);
                        
                        break;
                    case EStalkerStates.Patrol:
                        
                        view.SetCurrentSpeed(model.PatrolSpeed);

                        break;
                    case EStalkerStates.Chase:
                        
                        view.SetCurrentSpeed(model.ChaseSpeed);
                        
                        break;
                    case EStalkerStates.Flee:
                        view.SetCurrentSpeed(model.FleeSpeed);
                        break;
                    case EStalkerStates.Sleep:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
                }
                
                StateUpdate();
            });
        }

        public void PlayerTouched()
        {
            if (model.currentState.Value == EStalkerStates.Chase)
            {
                onPlayerTouched.Execute(model);                
            }
        }

        public void HubTouched()
        {
            model.currentState.Value = EStalkerStates.Flee;
        }

        public void PatrolFinished()
        {
            model.isLooking.Value = false;
            model.currentState.Value = EStalkerStates.Wander;
        }

        public void StateUpdate()
        {
            switch (model.currentState.Value)
            {
                case EStalkerStates.Wander:

                    if (view.IsCurrentPathCompleted())
                    {
                        view.SetNavMeshDestination(model.GetRandomWanderDestination());
                    } 
                    break;
                
                case EStalkerStates.Patrol:

                    if (model.isLooking.Value)
                        break;
                    
                    if (view.IsCurrentPathCompleted())
                    {
                        model.isLooking.Value = true;
                        view.StartCoroutine(view.LookArround());
                    }

                    break;
                case EStalkerStates.Chase:

                    if (model.PlayerEscaped(view.transform.position))
                    {
                        model.currentState.Value = EStalkerStates.Wander;
                    }
                    
                    view.SetNavMeshDestination(model.GetCurrentTarget().transform);
                    
                    break;
                
                case EStalkerStates.Flee:

                    if (view.IsCurrentPathCompleted())
                    {
                        model.currentState.Value = EStalkerStates.Wander;
                    }
                    break;
                case EStalkerStates.Sleep:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
