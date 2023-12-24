package com.demo.chedule.jobs;

import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;

import java.util.concurrent.TimeUnit;

@Component
public class DemoJob {
    @Scheduled(fixedRate = 5, initialDelay = 2, timeUnit = TimeUnit.SECONDS)
    public void jobDemo() {
        //fixedRate : Đặt một khoảng thời gian cố định giữa các lần thực hiện công việc.
        //initialDelay : Đặt một thời gian trễ trước khi bắt đầu công việc lần đầu tiên sau khi ứng dụng được khởi động.
            // VD ở đây là mở app lên thì sau 2 giây job sẽ chạy
        //timeUnit : Đơn vị thời gian
        System.out.println("Job FixedRate run...");
    }
    @Scheduled(fixedDelay = 5, initialDelay = 2, timeUnit = TimeUnit.SECONDS)
    public void jobDemoDelay() {
        // fixedDelay : Đặt một khoảng thời gian giữa thời điểm kết thúc của công việc trước và bắt đầu của công việc tiếp theo.
        System.out.println("Job Delay run...");
    }
    @Scheduled(cron = "0/5 * * * * *", zone = "Asia/Ho_Chi_Minh")
    public void jobDemoCron() {
        // cron: Cho phép bạn sử dụng biểu thức cron để xác định thời điểm thực hiện công việc.
        //zone: Xác định múi giờ (timezone) mà cron expression sẽ được thực hiện.
        System.out.println("Job cron run....");
    }
}
