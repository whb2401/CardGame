//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ball.Config {
  using ProtoBuf;
  
  
  [ProtoContract()]
  public class GlobalTemplate : IGeneratorObject {
    
    // 昵称长度限制
    [ProtoMember(1)]
    public int NameLengthLimit;
    
    public virtual void OnInit() {
    }
    
    public static GlobalTemplate Load() {
      return Load<GlobalTemplate>();
    }
    
    public static T Load<T>()
      where T : GlobalTemplate, new () {
      return GeneratorUtility.Load<T>("GlobalConfig");
    }
  }
}
