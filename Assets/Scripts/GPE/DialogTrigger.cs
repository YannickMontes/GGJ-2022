public class DialogTrigger : TriggerOnPlayer
{
    public DialogueInfosData dialog = null;

    protected override void OnPlayerTrigger(Player player)
    {
        UIManager.Instance.LaunchDialog(dialog);
    }
}
