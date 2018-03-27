##  解释 *游戏对象*（GameObjects） 和 *资源*（Assets）的区别与联系。
------
### 游戏对象(Game Objects)
由开发者通过unity3D创建的。具有cube、Point Light等不同的类别，但其均是由空对象(Empty)添加组件(component)变化而来。根据实现的不同，开发者可以自由选择添加不同的组件如脚本、声音等。

### 资源(Assets)
由他人或自己通过unity3D或者其他建模、绘图等软件创建。有Material、Scene等不同类型。可以是图片，模型或是unity3D可识别的其他资源文件。使用时可以将其作为游戏对象的某个组件， 或者将其作为一个新的对象显示在游戏界面中。
### 区别与联系
二者在来源及分类上有很大的不同，但是某些资源在一定条件下可以转化为游戏对象进行如脚本编写等的操作,而unity3D上的游戏制作主要依赖于通过游戏对象实现的游戏场景的变换和通过资源实现的渲染等操作，二者缺一不可。


##  编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件
--------
### 基本行为
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Cube : MonoBehaviour {

        void Awake() {
            Debug.Log ("Awake");
        }

        // Use this for initialization
        void Start () {
            Debug.Log ("start");
        }
        
        // Update is called once per frame
        void Update () {
            Debug.Log ("update");
        }

        void FixedUpdate() {
            Debug.Log ("Fixed Update");
        }
    
        void LateUpdate(){
            Debug.Log ("Later Update");
        }

    } 
利用上图所示的代码查看Awake() Start() Update() FixedUpdate() LateUpdate()的行为，结果如下：

![3-2][1]

  
 如图3-2所示， Awake()最先运行，Start()紧随其后，且这两个函数均只运行一次。根据函数名我们可以初步推断这两个函数均是用来初始化游戏对象，在此程序中没有特别的不同。实际上，Awake()函数是在该游戏对象加载脚本时被调用，而Start()函数是在Update()函数被调用之前被调用的。我们可以理解为Awake()函数将“沉睡”中的游戏对象唤醒，并将其之后要做什么告知该游戏对象，而Start()函数则是给出一个游戏对象可以开始运动的指令。

如图3-3所示，紧接着Start()函数，FixedUpdate()、Update()、LaterUpdate()被先后调用，初始的几帧内其顺序保持一致且在行为上没有什么差别。但随着游戏的继续运行，我们可以通过图3-4看出，FixedUpdate()调用频率相对于其他两个函数而言有较大的变化。这其实并不是FixedUpdate()函数调用频率增加，而是相对的Update()函数调用频率下降。
因为我们知道，随着游戏的运行，内存占用率会有一定的起伏，根据需要渲染与加载的游戏对象的多少，电脑的运行速度会有很大的变化，最直观的反应便是游戏帧率的改变，严重的甚至出现掉帧的情况。
	
而Update()函数是根据帧率被调用的，一般来讲，一帧即是Update()函数被调用一次，而FixedUpdate()则有严格的时间限制，在一定时间间隔后，FixedUpdate()即被强制调用。至于LaterUpdate()，从其函数名上我们就可以看出，它是在Update()与FixedUpdate()被调用之后调用的。因此在之后的运行中，一旦Update()被调用，LaterUpdate()就会被调用，而FixedUpdate()与两者调用的时间上无直接联系。依此我们可以知道，对于一般游戏中的碰撞或者快速运动而言，最好使用FixedUpdate()进行更新，而游戏的MainCamera则最好使用LaterUpdate()更新，防止物体的瞬移等影响游戏体验的情况出现。

### 常用事件

``` stylus
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    void Awake() {
        Debug.Log ("Awake");
    }

    // Use this for initialization
    void Start () {
        Debug.Log ("start");
    }
    
    // Update is called once per frame
    void Update () {
        //Debug.Log ("update");
    }

    void FixedUpdate() {
        //Debug.Log ("Fixed Update");
    }

    void LateUpdate(){
        //Debug.Log ("Later Update");
    }

    void OnGUI(){
        if (GUI.Button (new Rect (442, 200, 50, 50), click))
            Debug.Log ("Click");
    }

    void OnEnable(){
        Debug.Log ("Enable");
    }

    void OnDisable(){
        Debug.Log ("disable");
    }    
} 
```

![enter description here][3]

![enter description here][4]

如图3-6所示，OnEnable()函数在调用Awake()函数后被立即调用，则是意味着该Cube对象已被激活，可以执行接下来的进程。而我们利用OnGUI()函数在界面创建了一个只能在游戏测试时显示的半透明按钮“Click”，对于每一次的点击，都会调用。那么当我们把if语句注释掉之后，如图3-8所示

![enter description here][5]

OnGUI()即与Update()函数等无异，调用频率极高，且根据Console显示，其调用与其他函数没有直接关系，且基本都是连续调用两次后间隔一段时间，如图3-9所示：

![enter description here][6]

而OnDisable()函数则是在游戏对象被摧毁或者游戏结束时（其实二者对于游戏对象差别不大）调用的。如图3-7所示。

至此我们可以粗略推断出，Unity3D是通过高频率的函数调用的方法来实现动画上的基本连续。而类似于OnGUI()这样的函数则是玩家观感连续性的重要支柱。

## 查找脚本手册，了解 GameObject，Transform，Component 对象
-------
### Official Description
*GameObject: Base class for all entities in Unity scenes.*
游戏对象：一切Unity场景中实例的基础类
*Transform: Position, rotation and scale of an object.*
形变：对象的位置、旋转和比例
*Component: Base class for everything attached to GameObjects.*
组件：一切与游戏对象有关的基础类

![enter description here][7]

### table 对象（实体）
对于table 对象（实体）来说，我们可以看到其基本属性只有Name, Tag, Static, Layer和Prefab几栏，其中Name与Static较好理解，Name即为对象名，Static可以设置起是否在被调用后重置。而Tag则是一个人工添加的标签（虽然有系统默认值，但可以在setting中添加其他标签），Tag作为一个对于物体的标识，方便开发者调用相关函数及组件。而Layer，中文翻译为层次，可以简单地视为一个系统给游戏对象添加的标签，其中有water等属性，帮助开发者设置不同的物体类型。例如water即会改变物体的渲染模式与其对于光源的反射和透射，从而营造更加优秀的视觉效果。

至于Prefab中的三个预设值，分别解释如下：
Select可以在Assets中定位此对象，方便开发者对于对象进行修改，Revert则是将对象中的某些组件还原为默认值，能还原的如Transform中的Scale和Animation中的Animate Physics，不能还原的则有Transform中的Position与Rotation等设定。Apply从字面意义上理解即是应用，开发者更改的设定则可通过Apply进行应用，当然如上述提到的Revert不能还原的某些变量，在实例属性面板上，有些设置是实时保存的，因此这些设置的更改便不需要Apply。

### Transform
	
Transform中有三个设置，Position, Rotation与Scale，分别对应着对象的位置，旋转以及比例。我们一个一个来说：
	
首先，Position是指物体中心的位置，无论其他两个属性如何变动，物体的中心都是不会变的，我们在使物体移动的时候也是通过改变物体中心的坐标而实现的。因此在移动物体时要充分考虑到物体的宽度长度和高度，避免在改变坐标时有穿越或者重合的现象发生。
	
其次是Rotation设置。Unity3D中使用的xyz轴与一般的数学上使用的轴稍有不同。S竖直的为y轴，而前后为z轴，左右为x轴，旋转则是沿着轴的正方向顺时针旋转。注意其中填写的是度数，为360度制。
	
Scale则是各个方向的比例,根据等比例的增长或减少可以实现物体的放大与缩小，根据改变各数值可以实现物体形状的变化，如游戏界面中Floor的产生，即可以改变Cube-Scale的xyz值来改变。
	
![enter description here][8]

## 编写简单代码验证以下技术的实现
------
### 查找对象
`public static GameObject Find(string name)`
### 添加对象
`public static GameObject CreatePrimitive(PrimitiveTypetype)`
### 遍历对象
`foreach (Transform child in transform) {
         Debug.Log(child.gameObject.name);
}`

### 清除所有子对象
`foreach (Transform child in transform) {
 Destroy(child.gameObject);
}`

## 资源预设（Prefabs）与 对象克隆 (clone)
---------
### 预设（Prefabs）有什么好处？
预设相当于一个模板，可以批量产生相同属性的对象；修改预设后，通过预设创建的对象也会发生变化，对于批量处理很有帮助。

### 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
预设和克隆都能产生出和原对象相同的对象。但对象克隆是复制原对象，复制后即与原对象无关，因此改变原对象不会对克隆出的对象产生影响。

### 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象

## 尝试解释组合模式
--------
组合模式允许将对象组合成树形结构来表现“部分-整体”的层次结构，使得对象使用者以一致的方式处理单个对象以及对象的组合。



父类对象：

         void Start() {

                   BroadcastMessage("HelloWorld");

         }
		 
子类对象：

         void HelloWorld() {

                   print("HelloWorld!");

         }




  [1]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/Qz*fx82*19UKs**eNp1XqrNzIcYIUKhyVumMWMpCMLE!/b/dPIAAAAAAAAA&bo=7AOAAgAAAAADB08!&rf=viewer_4
  [2]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/Qz*fx82*19UKs**eNp1XqrNzIcYIUKhyVumMWMpCMLE!/b/dPIAAAAAAAAA&bo=7AOAAgAAAAADJ28!&rf=viewer_4
  [3]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/oQW.GEuPOL5EBuryT2BUHodUUHKYwiz.a5aBNGyahWk!/b/dEEBAAAAAAAA&bo=8QOAAgAAAAADJ3I!&rf=viewer_4
  [4]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/HJFVkzL*Z2tBM4EtzURZh4SyaZYXK.AwrqUtzhoGMw0!/b/dEABAAAAAAAA&bo=7QOAAgAAAAADJ24!&rf=viewer_4
  [5]:http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/Ex2HHSmXF3VxfYv3EV5xfZP.og.kY6YMiwnXOtrAjRo!/b/dPMAAAAAAAAA&bo=7AOAAgAAAAADJ28!&rf=viewer_4
  [6]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/zoJD.wUIlqk9DWJqE8SS3A.kl*33Yhrjgi.zxGx9tws!/b/dFYBAAAAAAAA&bo=6gOAAgAAAAADJ2k!&rf=viewer_4
  [7]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/zwlug3VEInK2ZbijCfO.HdxP5a1BL*ZeFzl8UBbWzk4!/b/dFYBAAAAAAAA&bo=kgOAAgAAAAADJxE!&rf=viewer_4
  [8]: http://m.qpic.cn/psb?/53f71654-6458-4891-9edb-89c9b2417582/kZRqme6Ue6xGVWb9tzh4ZBjJeQ7ddKm8Il6arHAEmrQ!/b/dHIAAAAAAAAA&bo=zAFAAQAAAAARB7w!&rf=viewer_4