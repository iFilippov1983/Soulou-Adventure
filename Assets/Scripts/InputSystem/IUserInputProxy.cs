using System;

namespace InputSystem
{
    public interface IUserInputProxy
    {
        event Action<float> OnAxisChange;
        void GetAxis();
    }
}
