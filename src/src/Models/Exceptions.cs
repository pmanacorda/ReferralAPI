public class UserNameNotFoundException : Exception{
    public UserNameNotFoundException(){}
    public UserNameNotFoundException(string msg) : base(msg){}
    public UserNameNotFoundException(string msg, Exception inner) : base(msg, inner){}

    public readonly Message Msg = new Message{
        Code = MESSAGE_CODE.DATA_NOT_FOUND,
        Description = "The username was not found or is invalid"
    };
}

public class ConfigurationNotFoundException : Exception{
    public ConfigurationNotFoundException(){}
    public ConfigurationNotFoundException(string msg) : base(msg){}
    public ConfigurationNotFoundException(string msg, Exception inner) : base(msg, inner){}

    public readonly Message Msg = new Message{
        Code = MESSAGE_CODE.DATA_NOT_FOUND,
        Description = "The configuration was not found or is invalid"
    };
}