using Gameplay.Cameras;
using Gameplay.Character;
using Gameplay.Items;
using Gameplay.Timing;

public class GameplayManagersParent : ManagersParent
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected override void SetManagers()
    {
        managers.Add(ItemsManager.Instance);
        managers.Add(CharacterManager.Instance);
        managers.Add(CamerasManager.Instance);
        managers.Add(TimeManager.Instance);
    }

    #endregion
}
