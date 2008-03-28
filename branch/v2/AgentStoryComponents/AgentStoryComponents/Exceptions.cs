using System;
using System.Collections.Generic;
using System.Text;

namespace AgentStoryComponents
{
    public class UserExistsException : Exception
    {
        public UserExistsException(string msg)
            : base(msg)
        {}
    }
    public class UserMayNotClone : Exception
    {
        public UserMayNotClone(string msg)
            : base(msg)
        { }
    }
    
    public class InvalidGroupActionException : Exception
    {
     public InvalidGroupActionException(string msg)
            : base(msg)
        {}
    }

    public class InsufficientPermissionsException : Exception
    {
        public InsufficientPermissionsException(string msg)
            : base(msg)
        {}
    }

    public class MessageNotSentException : Exception
    {
        public MessageNotSentException(string msg)
            : base(msg)
        { }
    }
    public class ServiceTimeoutException : Exception
    {
        public ServiceTimeoutException(string msg)
            : base(msg)
        { }
    }

    public class UnableToDeleteUserException : Exception
    {
        public UnableToDeleteUserException(string msg)
            : base(msg)
        { }
    }


    public class CommandExecuteException : Exception
    {
        public CommandExecuteException(string msg)
            : base(msg)
        { }
    }

    public class PossibleHackException : Exception
    {
        public PossibleHackException(string msg)
            : base(msg)
        { }
    }

    public class InvitationDoesNotExistException : Exception
    {
        public InvitationDoesNotExistException(string msg)
            : base(msg)
        {}
    }

    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string msg)
            : base(msg)
        {}
    }
    public class GroupDoesNotExistException : Exception
    {
        public GroupDoesNotExistException(string msg)
            : base(msg)
        { }
    }
  public class GroupExistsException : Exception
    {
      public GroupExistsException(string msg)
            : base(msg)
        { }
    }
    public class CommandDoesNotExistException : Exception
    {
        public CommandDoesNotExistException(string msg)
            : base(msg)
        { }
    }
    public class ParameterNotFoundException : Exception
    {
        public ParameterNotFoundException(string msg)
            : base(msg)
        { }
    }
    public class ParameterNotNumberException : Exception
    {
        public ParameterNotNumberException(string msg)
            : base(msg)
        { }
    }

}
