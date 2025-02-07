namespace NotsGame.Core.EventSystem
{
    public delegate void EventListener<in TEvent>(object sender, TEvent e);
}