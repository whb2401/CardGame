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
  public class DuplicateTemplate : IGeneratorObject {
    
    // 索引
    [ProtoMember(1)]
    public int Id;
    
    // 地图
    [ProtoMember(2)]
    public string MapPath;
    
    // 类型
    [ProtoMember(3)]
    public int Type;
    
    // 名称
    [ProtoMember(4)]
    public string Name;
    
    // 开始索引
    [ProtoMember(5)]
    public int StartIndex;
    
    public virtual void OnInit() {
    }
    
    public static DuplicateTemplate[] Load() {
      return Load<DuplicateTemplate>();
    }
    
    public static T[] Load<T>()
      where T : DuplicateTemplate, new () {
      return GeneratorUtility.Load<T>("DuplicatesConfig", "Duplicate");
    }
  }
}