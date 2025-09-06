FROM microsoft/dotnet:2.1-aspnetcore-runtime

WORKDIR /app
COPY . .

VOLUME ["/app/wwwroot"]
VOLUME ["/app/App_Data"]

ENTRYPOINT ["dotnet", "MQKJ.BSMP.Web.Mvc.dll"]
