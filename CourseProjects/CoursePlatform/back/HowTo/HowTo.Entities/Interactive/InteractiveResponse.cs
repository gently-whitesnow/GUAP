namespace HowTo.Entities.Interactive;

public class InteractiveResponse
{
    public InteractiveResponse(InteractivePublic interactive, LastInteractivePublic lastInteractive)
    {
        Interactive = interactive;
        LastInteractive = lastInteractive;
    }
    public InteractivePublic Interactive { get; set; }
    public LastInteractivePublic LastInteractive { get; set; }
}