using Abp.Application.Navigation;
using Abp.Localization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class BSMPNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        new FixedLocalizableString(PageNames.Home),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                )
                //用户管理
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Players,
                        L("PlayerManager"),
                        url: "Players",
                        icon: "people"
                    )
                )

                //支付
                //.AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Players,
                //        L("Pay"),
                //        url: "Pay",
                //        icon: "people"
                //    )
                //)
                 //中国宝宝
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.AgentManage,
                        L("ChineseBabies"),
                        url: "ChineseBabies",
                        icon: "assessment",
                        requiresAuthentication: true
                        ).AddItem(
                        new MenuItemDefinition(
                            "AgentManage",//代理管理
                            L("AgentManage"),
                            icon: "business",
                            requiresAuthentication: true
                            ).AddItem(
                            new MenuItemDefinition(
                            "AgentManage",
                            L("Agent"),
                            url: "ChineseBabies/Agent"
                                ))
                            .AddItem(new MenuItemDefinition(
                            "AgentManage",
                            L("AgentInviteCode"),
                            url: "ChineseBabies/AgentInviteCode"
                        )).AddItem(new MenuItemDefinition(
                            "AgentManage",
                            L("WithdrawMoney"),
                            url: "ChineseBabies/WithdrawMoney"
                        )).AddItem(new MenuItemDefinition(
                            "RunWaterRecord",
                            L("RunWaterRecord"),
                            url: "ChineseBabies/RunWaterRecord"
                        )).AddItem(new MenuItemDefinition(
                            "AgentManage",
                            L("AgentFamily"),
                            url: "ChineseBabies/AgentFamily"
                        ))
                        ).AddItem(new MenuItemDefinition(
                            "UploadsData",
                            L("UploadsData"),
                            icon: "business",
                            url: "ChineseBabies/ImportData"
                            )).AddItem(
                        new MenuItemDefinition(
                            "OrderManage",//订单管理
                            L("OrderManage"),
                            icon: "business",
                            requiresAuthentication: true
                        ).AddItem(new MenuItemDefinition(
                            "OrderManage",
                            L("OrderManage"),
                            url: "ChineseBabies/Order"
                        ))
                        ).AddItem(
                        new MenuItemDefinition(
                            "FamilyManage",//家庭管理
                            L("FamilyManage"),
                            icon: "business",
                            requiresAuthentication: true
                        ).AddItem(new MenuItemDefinition(
                            "FamilyManage",
                            L("FamilyManage"),
                            url: "ChineseBabies/Family"
                        ))
                        ).AddItem(
                        new MenuItemDefinition(
                            "AgentIncome",//代理业绩
                            L("AgentIncomeManage"),
                            icon: "business",
                            requiresAuthentication: true
                        ).AddItem(new MenuItemDefinition(
                            "AgentIncome",
                            L("AgentIncome"),
                            url: "ChineseBabies/AgentIncome"
                        ))
                    ))

                //任务管理
                .AddItem(new MenuItemDefinition(
                        "GameTaskManager",
                        L("GameTaskManager"),
                        icon: "schedule",
                        requiresAuthentication: true
                    ).AddItem(new MenuItemDefinition(
                        PageNames.GameTasks,
                        L("GameTasks"),
                        url: "GameTasks"
                    )).AddItem(new MenuItemDefinition(
                        PageNames.BonusPoints,
                        L("BonusPoints"),
                        url: "bonuspoints"
                        ))
                )

                //内容管理
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Content,
                        L("ContentManager"),
                        icon: "favorite",
                        requiresAuthentication: true
                    ).AddItem(new MenuItemDefinition(
                            PageNames.Questions,
                        L("Questions"),
                        url: "Questions"
                    //icon: "favorite"
                    )).AddItem(
                    new MenuItemDefinition(
                        PageNames.Scenes,
                        L("Scenes"),
                        url: "Scenes"
                    //icon: "local_offer"
                    //requiredPermissionName:PermissionNames.Pages_Scenes
                    ))
                    //.AddItem(new MenuItemDefinition(
                    //    "TopicManager",
                    //    L("Topics"),
                    //    url: "topics"
                    //))
                    .AddItem(new MenuItemDefinition(
                        PageNames.Tags,
                        L("TagsManager"),
                        //icon: "flag",
                        requiresAuthentication: true
                    ).AddItem(new MenuItemDefinition(
                        "TagTypeManager",
                        L("TagType"),
                        url: "tagtype"
                    //requiredPermissionName:PermissionNames.
                    )).AddItem(new MenuItemDefinition(
                        "Tag Manager",
                        L("Tags"),
                        url: "tag"
                        ))
                ))

                //活动报名
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.RiskActiveApply,
                        L("RiskActiveApply"),
                        url: "RiskActiveApply",
                        icon: "assessment"
                    )
                )

                //广告管理
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.AD,
                        L("Adviertisement"),
                        url: "Adviertisement",
                        icon: "assessment",
                        requiresAuthentication: true
                        ).AddItem(
                        new MenuItemDefinition(
                            PageNames.AD, //模拟夫妻互动
                            L("Adviertisement"),
                            url: "Adviertisements/Adviertisement",
                            requiresAuthentication: true
                            )
                        )
                )

                 //报名
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.AgentManage,
                        L("Activities"),
                        url: "Activities",
                        icon: "assessment",
                        requiresAuthentication: true
                        ).AddItem(
                        new MenuItemDefinition(
                            PageNames.SimulationSpouse, //模拟夫妻互动
                            L("SimulationSpouse"),
                            url: "Activities/SimulationSpouse",
                            requiresAuthentication: true
                            )
                        )
                )

                //数据统计
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.DataStatistics,
                        L("DataStatistics"),
                        url: "DataStatistics",
                        icon: "assessment",
                        requiresAuthentication: true
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.PassModel,//闯关统计
                            L("PassModel"),
                            icon: "business",
                            requiresAuthentication: true
                            ).AddItem(
                            new MenuItemDefinition(
                            "OpeningStatistics",
                            L("OpeningStatistics"),
                            url: "PassModel/OpeningStatistics"
                                )).AddItem(
                            new MenuItemDefinition(
                            "LevelStatistics",
                            L("LevelStatistics"),
                            url: "PassModel/LevelStatistics"
                                )).AddItem(
                            new MenuItemDefinition(
                            "AnswerQuestionStatistics",
                            L("AnswerQuestionStatistics"),
                            url: "PassModel/AnswerQuestionStatistics"
                                ))
                        ).AddItem(
                        new MenuItemDefinition(
                            PageNames.GamblingModel,//赌约模式
                            L("GamblingModel"),
                            icon: "business",
                            requiresAuthentication: true
                            ).AddItem(
                            new MenuItemDefinition(
                            "OpeningStatistics",
                            L("OpeningStatistics"),
                            url: "GamblingModel/OpeningStatistics"
                                )).AddItem(
                            new MenuItemDefinition(
                            "ErrorQuestionStatistics",
                            L("ErrorQuestionStatistics"),
                            url: "GamblingModel/ErrorQuestionStatistics"
                                )).AddItem(
                            new MenuItemDefinition(
                            "AnswerQuestionStatistics",
                            L("AnswerQuestionStatistics"),
                            url: "GamblingModel/AnswerQuestionStatistics"
                                ))
                        ).AddItem(
                        new MenuItemDefinition(
                            PageNames.QuestionStatistics,//题目统计
                            L("QuestionStatistics"),
                            url: "QuestionStatistics/QuestionStatistics",
                            icon: "business",
                            requiresAuthentication: true
                            ))
                )

                //测试
                .AddItem(
                    new MenuItemDefinition(
                            PageNames.DebugModel,
                            L("DebugModel"),
                            icon: "settings",
                            requiresAuthentication: true
                        ).AddItem(
                            new MenuItemDefinition(
                                    "ModifyFloor",
                                    L("ModifyFloor"),
                                    url: "DebugModel/ModifyFloor"
                                )
                        ).AddItem(new MenuItemDefinition("ModifyGrown",
                            L("ModifyGrown"),
                            url: "DebugModel/Modifygrown"))
                )

                //恋爱卡片管理
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.CardManage,
                        L("CardManage"),
                        url: "CardManage",
                        icon: "assessment"
                    //requiredPermissionName: PermissionNames.Pages_Users
                    )
                )

                 //系统管理
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.System,
                        L("SystemManager"),
                        icon: "settings",
                        requiresAuthentication: true
                    ).AddItem(new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users"
                    //icon: "settings"
                    )).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles"))
                    //icon: "local_offer"
                    //requiredPermissionName:PermissionNames.Pages_Scenes
                    //)).AddItem(new MenuItemDefinition(
                    //    "AuthorityManager",
                    //    L("Authoritys"),
                    //    url: "Authoritys"
                    //    //icon: "local_offer"
                    //    ))
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.Tenants,
                            L("Tenants"),
                            url: "Tenants",
                            icon: "business",
                            requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                    )
                );

            //标签管理
            //.AddItem(new MenuItemDefinition(
            //        PageNames.Tags,
            //        L("TagsManager"),
            //        icon: "flag",
            //        requiresAuthentication: true
            //    ).AddItem(new MenuItemDefinition(
            //        "TagTypeManager",
            //        L("TagType"),
            //        url: "tagtype"
            //    )).AddItem(new MenuItemDefinition(
            //        "Tag Manager",
            //        L("Tags"),
            //        url: "tag"
            //        ))
            //);
            //.AddItem(
            //    new MenuItemDefinition(
            //        PageNames.Question,
            //        new FixedLocalizableString("问答管理"),
            //        url: "Questions",
            //        icon: "favorite"
            //    )
            //)
            //.AddItem(
            //    new MenuItemDefinition(
            //        PageNames.Users,
            //        new FixedLocalizableString("用户管理"),
            //        url: "Users",
            //        icon: "people",
            //        requiredPermissionName: PermissionNames.Pages_Users
            //    )
            //).AddItem(
            //    new MenuItemDefinition(
            //        PageNames.Roles,
            //        new FixedLocalizableString("角色管理"),
            //        url: "Roles",
            //        icon: "local_offer",
            //        requiredPermissionName: PermissionNames.Pages_Roles
            //    )
            //).AddItem(
            //    new MenuItemDefinition(
            //        PageNames.Tenants,
            //        new FixedLocalizableString("租户管理"),
            //        url: "Tenants",
            //        icon: "business",
            //        requiredPermissionName: PermissionNames.Pages_Tenants
            //    )
            //)

            //.AddItem(
            //    new MenuItemDefinition(
            //        PageNames.About,
            //        L("About"),
            //        url: "About",
            //        icon: "info"
            //    )
            //)
            //.AddItem( // Menu items below is just for demonstration!
            //    new MenuItemDefinition(
            //        "MultiLevelMenu",
            //        L("MultiLevelMenu"),
            //        icon: "menu"
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "AspNetBoilerplate",
            //            new FixedLocalizableString("ASP.NET Boilerplate")
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateHome",
            //                new FixedLocalizableString("Home"),
            //                url: "https://aspnetboilerplate.com?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateTemplates",
            //                new FixedLocalizableString("Templates"),
            //                url: "https://aspnetboilerplate.com/Templates?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateSamples",
            //                new FixedLocalizableString("Samples"),
            //                url: "https://aspnetboilerplate.com/Samples?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateDocuments",
            //                new FixedLocalizableString("Documents"),
            //                url: "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl"
            //            )
            //        )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "AspNetZero",
            //            new FixedLocalizableString("ASP.NET Zero")
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroHome",
            //                new FixedLocalizableString("Home"),
            //                url: "https://aspnetzero.com?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroDescription",
            //                new FixedLocalizableString("Description"),
            //                url: "https://aspnetzero.com/?ref=abptmpl#description"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroFeatures",
            //                new FixedLocalizableString("Features"),
            //                url: "https://aspnetzero.com/?ref=abptmpl#features"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroPricing",
            //                new FixedLocalizableString("Pricing"),
            //                url: "https://aspnetzero.com/?ref=abptmpl#pricing"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroFaq",
            //                new FixedLocalizableString("Faq"),
            //                url: "https://aspnetzero.com/Faq?ref=abptmpl"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroDocuments",
            //                new FixedLocalizableString("Documents"),
            //                url: "https://aspnetzero.com/Documents?ref=abptmpl"
            //            )
            //        )
            //    )
            // );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BSMPConsts.LocalizationSourceName);
        }
    }
}
