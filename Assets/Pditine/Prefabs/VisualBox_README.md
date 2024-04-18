# VisualBox

## 使用

在需要触发Feedback的时候如下调用Act函数即可

``` c#
public void Act(
    float? shakeRange = null, 
    int? disperseRange = null, 
    float? disperseRate = null)
```

## 参数说明

三个参数都是可选值，如果不显式给予会赋予默认值（默认值在inspector中进行指定）

- shakeRange：第一个被触发的MMScaleShaker的初始振幅
- disperseRange：震动会传递给多少周围的方块
- disperseRate：每次传递震动的衰减率

## 原理说明

在游戏开始时，每个方块会向四周各方向（rayDirection）发出射线，检测与自己相同的方块，如果检测到会将其存储在boxesAround数组中，形成连接

Act函数在执行逻辑后会触发boxesAround中其他方块的Act函数，形成类似传递的效果