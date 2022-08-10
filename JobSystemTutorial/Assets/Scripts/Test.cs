using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;

public class Test : MonoBehaviour
{
    [SerializeField] bool use_job = false;
    private void Update() {
        float start_time = Time.realtimeSinceStartup;
        if(use_job){
            JobHandle handle = ReallyToughTaskJobbed();
            handle.Complete();
        }
        else{
            ReallyToughTask();
        }
        print((Time.realtimeSinceStartup - start_time) * 1000f + "ms");
    }
    void ReallyToughTask(){
        float num = 0;
        for(int i = 0; i < 500000; i++){
            num = Mathf.Exp(Mathf.Sqrt(i));
        }
    }
    JobHandle ReallyToughTaskJobbed(){
        ReallyToughOccupation job = new ReallyToughOccupation();
        return job.Schedule();
    }
}
[BurstCompile]
public struct ReallyToughOccupation : IJob{
    public void Execute(){
        float num = 0;
        for(int i = 0; i < 50000; i++){
            num = Mathf.Exp(Mathf.Sqrt(i));
        }
    }
}
