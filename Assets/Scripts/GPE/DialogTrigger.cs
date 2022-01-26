public class DialogTrigger : TriggerOnPlayer
{
    public DialogueInfosData dialog = null;

    protected override void OnPlayerTriggerEnter(Player player)
    {
        UIManager.Instance.LaunchDialog(dialog);
    }
}
