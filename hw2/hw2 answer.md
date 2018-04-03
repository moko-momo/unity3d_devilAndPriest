### 游戏对象运动的本质是什么？
--------------

本质为游戏对象每帧的运动，每帧的变换使物体发生运动。物体的运动具体由transform属性中的position跟rotation进行调整，两者分别决定物体的位置与旋转角度。



### 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）
---------


1 让物体同时在两个方向产生位移，一个是初速度方向，一个是垂直方向，即利用position属性进行位移调整：
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
  
	public class move : MonoBehaviour {  
  
    public float speed = 1;  
   	 // Use this for initialization  
    void Start () {  
        Debug.Log("start!");  
    }  
      
    // Update is called once per frame  
    void Update () {  
  		 transform.position += Vector3.right * Time.deltaTime * 3;  
	transform.position += Vector3.down * Time.deltaTime * speed;  
        speed++;  
 	   }  
	}  

2 通过创建一个Vector3变量实现抛物线，使其竖直方向上不断增加，水平方向上不变。

	using System.Collections;  
	using System.Collections.Generic;  
	using UnityEngine;  
  
	public class move : MonoBehaviour {  
  
    public float speed = 1;  
    // Use this for initialization  
    void Start () {  
        Debug.Log("start!");  
    }  
      
    // Update is called once per frame  
    void Update () {  
  
        Vector3 ve = new Vector3( Time.deltaTime*3, -Time.deltaTime*speed, 0);  
        transform.position += ve;  
        speed++;  
    	}  
	}  

3.同样使用vector3，但改变position时利用translate函数；

public class move : MonoBehaviour {  
  
    public float speed = 1;  
    // Use this for initialization  
    void Start () {  
        Debug.Log("start!");  
    }  
      
    // Update is called once per frame  
    void Update () {  
  
        Vector3 ve = new Vector3( Time.deltaTime*3, -Time.deltaTime*speed, 0);  
        transform.Translate(ve);   
        speed++;  
 	   }  
	}  

### 写一个程序，实现一个完整的太阳系，其他星球围绕太阳的转速必须不一样，且不在一个法平面上。

------------


1.先创建太阳、月亮与八大行星，按照太阳系里的位置调整position；
2.装饰星球外观，创建material，添加下载的素材然后分别拖到各个星球上；
3.写出公转与自转的脚本，拖到星球上；

公转代码：
```using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate: MonoBehaviour {
	public Transform origin;
	public float speed = 20;
	float ry, rz;

	void Start() {
		ry = Random.Range(1, 360);
		rz = Random.Range(1, 360);
	}

	void Update() {
		Vector3 axis = new Vector3(0, ry, rz);
		this.transform.RotateAround(origin.position, axis, speed*Time.deltaTime);
	}
}
```

自转代码：

```using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class earth: MonoBehaviour {  

	// Use this for initialization  
	void Start () {  

	}  

	// Update is called once per frame  
	void Update () {  
		this.transform.RotateAround(this.transform.position, Vector3.up, 2);  
	}  
}  
```
4.添加component，加入行星轨道；添加背景,完成。
 录制视频见polar system.mp4.