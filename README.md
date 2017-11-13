# Declaimer
> 针对RFID读取器的基本操作的封装

###### 已完成的工作
- *完成对Zebra和Intermec Fixed Reader的封装*
- 

###### 接下来的工作
- *为项目增加日志记录借助Log4Net*

###### 项目文件夹解释

- **Declaimer**       --- *Winfrom示例项目，引用第三方框架* [MetroFramework](https://github.com/MassimoLoi/ModernUI)
- **DeclaimerCommon** --- *共同部分，枚举、方法、日志*
-  **DeclaimerFactory** --- Reader对象实例 创建工厂
-  **WaitingGodotDeclaimer** --- *Fixed Reader封装类*