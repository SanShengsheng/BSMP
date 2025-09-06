using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.HttpContextHelper;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Orders.Authorization;
using MQKJ.BSMP.Orders.Dtos;
using MQKJ.BSMP.Web.Areas.ChineseBabies.Models;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [AbpMvcAuthorize(OrderPermissions.Node)]
    [Area("ChineseBabies")]
    public class OrderController : BSMPControllerBase
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private readonly IOrderAppService _orderAppService;
        private readonly ICoinRechargeAppService _coinService;
        private readonly IMiniappService _miniappService;

        public OrderController(IOrderAppService orderAppService,
            ICoinRechargeAppService coinService,
            IMiniappService miniappService)
        {
            _orderAppService = orderAppService;
            _coinService = coinService;

            _miniappService = miniappService;
        }

        public IActionResult Index()
        {
            //var pageNumber = page ?? 1;
            //var pageSize = 10;

            //var result = await _orderAppService.GetPaged(new GetOrdersInput()
            //{
            //    SkipCount = (pageNumber - 1) * pageSize,
            //    StartTime = startTime ?? DateTime.Now.AddDays(-7),
            //    EndTime = endTime ?? DateTime.Now,
            //    UserName = userName,
            //    OrderState = state
            //});

            //var orders = new StaticPagedList<OrderListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_List", orders);
            //}
            //else
            //{
               
            //}

            return View();
        }

        [AbpMvcAuthorize(OrderPermissions.ExportToExcel)]
        [HttpPost]
        public IActionResult ExportToExcel(GetOrdersInput input)
        {
              var result = _orderAppService.GetToExcel(input);

            return Content(result);


            //Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.Headers.Add("content-disposition", string.Format("attachment;filename={0}-{1}.xlsx", "列表", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            //return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            //保存和归来的Excel文件作为一个byte数组
            //HttpResponse response = CustomHttpContext.Current.Response;
            //response.Clear();
            //response.Headers.Add("content-disposition", "attachment;  filename=order.xlsx");
            //response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //await response.WriteAsync(data.ToString(), Encoding.UTF8);
            //File("", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "");
        }

        [AbpMvcAuthorize(OrderPermissions.QueryOrders)]
        [HttpPost]
        public async Task<OrderModel> Search(GetOrdersInput request)
        {
            var recharges = await _coinService.PageSearch(new BSMP.ChineseBabies.Dtos.GetCoinRechargesInput()
            {
            });

            var orderList = await _orderAppService.GetPaged(request);

            var orderModel = new OrderModel();

            orderModel.OrderListDto = orderList;

            orderModel.CoinRechargeListDto = recharges;

            orderModel.Tenants = _miniappService.GetTenants();

            return orderModel;
        }

        [AbpMvcAuthorize(OrderPermissions.QueryOrderState)]
        [HttpPost]
        public Task<UpdateOrderStateOutput> QueryOrderState(BSMP.ChineseBabies.CoinRecharges.Dtos.UpdateOrderStateInput input) 
            => _coinService.QueryOrderState(input);
    }
}