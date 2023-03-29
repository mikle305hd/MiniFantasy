using Additional.Abstractions.States;
using Infrastructure.Game;
using Infrastructure.Services.Fps;

namespace Infrastructure.States
{
    public class SettingsLoadingState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IFpsService _fpsService;

        public SettingsLoadingState(GameStateMachine stateMachine, IFpsService fpsService)
        {
            _stateMachine = stateMachine;
            _fpsService = fpsService;
        }
        
        public void Enter()
        {
            SetApplicationDefaultSettings();
            EnterProgressLoadingState();
        }

        public void Exit()
        {
        }

        private void SetApplicationDefaultSettings()
        {
            _fpsService.SetTargetFpsUnlimited();
        }

        private void EnterProgressLoadingState() 
            => _stateMachine.Enter<ProgressLoadingState>();
    }
}