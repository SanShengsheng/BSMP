
#代码生成器(ABP Code Power Tools )使用说明文档

**52ABP官方网站：[http://www.52abp.com](http://www.52abp.com)**

>欢迎您使用 ABP Code Power Tools ，.net core 版本。
开发代码生成器的初衷是为了让大家专注于业务开发，
而基础设施的地方，由代码生成器实现，节约大家的实现。
实现提高效率、共赢的局面。

欢迎到：[Github地址](https://github.com/52ABP/52ABP.CodeGenerator) 提供您的脑洞，
如果合理的我会实现哦~

# 使用说明:

**配置Automapper** :

复制以下代码到Application层下的：BSMPApplicationModule.cs
中的 PreInitialize 方法中:

```
// 自定义类型映射
// 如果没有这一段就把这一段复制上去
Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
{
    // ....

    // 只需要复制这一段
MqAgentMapper.CreateMappings(configuration);

    // ....
});

```

**配置权限功能**  :

如果你生成了**权限功能**。复制以下代码到 BSMPApplicationModule.cs
中的 PreInitialize 方法中:

```
Configuration.Authorization.Providers.Add<MqAgentAuthorizationProvider>();

```

**EntityFramework功能配置**:

可以在```DbContext```中增加：

 ```
public DbSet<MqAgent>  MqAgents { get; set; }

 ```

在方法```OnModelCreating```中添加

```
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MqAgentCfg());
        }

```


**多语言配置**  

.Core 层下 Localization->SourceFiles 中

```

<text name="TenantId"  value="TenantId"></text>
<text name="PlayerId"  value="PlayerId"></text>
<text name="Level"  value="Level"></text>
<text name="State"  value="State"></text>
<text name="InviteCode"  value="InviteCode"></text>
<text name="Tenant"  value="Tenant"></text>
<text name="Player"  value="Player"></text>
<text name="UserName"  value="UserName"></text>
<text name="IdCardNumber"  value="IdCardNumber"></text>
<text name="PhoneNumber"  value="PhoneNumber"></text>
<text name="ParentInviteCode"  value="ParentInviteCode"></text>
<text name="GroupId"  value="GroupId"></text>
<text name="OpenId"  value="OpenId"></text>
<text name="UnionId"  value="UnionId"></text>
<text name="Password"  value="Password"></text>
<text name="UpperLevelMqAgent"  value="UpperLevelMqAgent"></text>
<text name="UpperLevelMqAgentId"  value="UpperLevelMqAgentId"></text>
<text name="WithdrawMoneyState"  value="WithdrawMoneyState"></text>
<text name="Balance"  value="Balance"></text>
<text name="TotalBalance"  value="TotalBalance"></text>
<text name="LockedBalance"  value="LockedBalance"></text>
<text name="AgentWithdrawalRatio"  value="AgentWithdrawalRatio"></text>
<text name="PromoterWithdrawalRatio"  value="PromoterWithdrawalRatio"></text>
<text name="HeadUrl"  value="HeadUrl"></text>
<text name="NickName"  value="NickName"></text>


<text name="MqAgent" value=""></text><text name="QueryMqAgent"  value="查询"></text><text name="CreateMqAgent"  value="添加"></text><text name="EditMqAgent"  value="编辑"></text><text name="DeleteMqAgent"  value="删除"></text><text name="BatchDeleteMqAgent" value="批量删除"></text><text name="ExportMqAgent"  value="导出"></text>                             

```




 **路线图**

todo: 目前优先完成SPA 以angular 为主，
如果你有想法我替你实现前端生成的代码块。
那么请到github 贴出你的代码段。
我感兴趣的话，会配合你的。

[https://github.com/52ABP/52ABP.CodeGenerator](https://github.com/52ABP/52ABP.CodeGenerator) 提供您的脑洞，

已完成：
- [x ]SPA版本的前端

待办：
- [ ]暂时搞不定注释，后期想办法
- [ ]菜单栏问题，如果是MPA版本
- [ ]MPA版本的前端
## 广告

52ABP官方网站：[http://www.52abp.com](http://www.52abp.com)

代码生成器帮助文档：[http://www.cnblogs.com/wer-ltm/p/8445682.html](http://www.cnblogs.com/wer-ltm/p/8445682.html)

【ABP代码生成器交流群】：104390185（收费）
[![52ABP .NET CORE 实战群](http://pub.idqqimg.com/wpa/images/group.png)](http://shang.qq.com/wpa/qunwpa?idkey=3f301fa3101d3201c391aba77803b523fcc53e59d0c68e6eeb9a79336c366d92)

【52ABP .NET CORE 实战群】：633751348 (免费)
[![52ABP .NET CORE 实战群](http://pub.idqqimg.com/wpa/images/group.png)](https://jq.qq.com/?_wv=1027&k=5pWtBvu)
