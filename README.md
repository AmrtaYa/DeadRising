本项目包含：
数据库(Sqlite) --> GameMainDB  类去进行 增删改查
Bilibili弹幕网站接口,在Game Main Eengie 类中进行回调函数初始化，拥有：送礼回调，发弹幕回调等
FSM有限状态机，继承(FSMTrigger与FSMState即可)接口实现，并在CSV中配置即可
CSV资源，数值配置文件读取工具，根据此仓库目录路径，能够利用ConfigCSV类进行读取
商店(还没写=。=)
其余业务逻辑请自行实现


后续FSM将实现可视化，类似U3D动画状态机
