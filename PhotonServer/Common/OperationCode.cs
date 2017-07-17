

namespace Common
{
    public enum OperationCode : byte //区分请求和响应的类型
    {
        Login,
        Register,
        Default,
        Match,  //匹配
        Ready,  //准备
    }
}
