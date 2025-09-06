@echo off
chcp 65001
set lpn
echo 请输入你要要使用的配置（以逗号分割，最后一组），输入：Test即可(注意大小写),使用默认的直接回车。参考链接：https://www.cnblogs.com/jackyfei/p/9938758.html
set /p env_name=
if defined env_name set lpn=%env_name%
@echo on
start "BSMP.API.%lpn%" dotnet watch  run  -c Debug --launch-profile=%lpn% 