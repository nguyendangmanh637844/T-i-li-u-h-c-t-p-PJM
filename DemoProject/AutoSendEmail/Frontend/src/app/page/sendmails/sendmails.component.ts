import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';
import { MailService } from 'src/app/service/mail.service';

@Component({
  selector: 'app-sendmails',
  templateUrl: './sendmails.component.html',
  styleUrls: ['./sendmails.component.scss']
})
export class SendmailsComponent implements OnInit {

  //Thời gian buổi sáng
  hourDay:number=7;
  minuteDay:number = 30;

  //Thời gian buổi tối
  hourNight:number= 22;
  minuteNight:number = 30;

  //Thời gian random
  hourRan:number = new Date().getHours();
  minuteRan:number = new Date().getMinutes()+1;

  //Thời gian giữa các lần quét
  second: number = 1000; //1s
  secondLog:number = 3*1000; //1p

  ranNum:number = 0;
  constructor(public service:MailService) { }
  ngOnInit(): void {
    //Log ra lần đầu sau 3s
    setTimeout(()=>{
    console.log("Day: "+ this.hourDay+"-"+this.minuteDay+ " | Night: "+ this.hourNight+"-"+this.minuteNight +" | Random: "+ this.hourRan+"-"+this.minuteRan);
    },this.secondLog);

    //1s quét một lần
    setInterval(()=>{
      if(this.isReady(new Date().getHours(), new Date().getMinutes(), new Date().getSeconds())==1){
      this.call();
      }
      if(this.isReady(new Date().getHours(), new Date().getMinutes(), new Date().getSeconds())==2){
        this.callForRan();
      }
    },this.second);
  }

  call(){
    this.service.send().subscribe(
        res =>
        {
          if(res) console.log("At default hour: "+new Date().getHours()+"-"+new Date().getMinutes()+" => Sent!!!");
          else console.log("At default hour: "+new Date().getHours()+"-"+new Date().getMinutes()+" => Fail!!!");
        }
      );

  }
  callForRan(){
    this.service.send().subscribe(
        res =>
        {
          if(res){
            console.log("At random hour: "+new Date().getHours()+"-"+new Date().getMinutes()+" => Sent!!");
            this.ranNum= Math.round(this.getRandomArbitrary(0, 2));
            if(this.ranNum==0) this.minuteRan = Math.round(this.getRandomArbitrary(this.minuteRan, 60));
            else this.minuteRan = Math.round(this.getRandomArbitrary(1,60));
            this.hourRan += this.ranNum;
            if(this.hourRan == 12 && this.minuteRan>30 || this.hourRan==13) this.hourRan=14;
            if(this.hourRan>22 || this.hourRan==this.hourNight && this.minuteRan==this.minuteNight) this.hourRan = 8;
            console.log("Day: "+ this.hourDay+"-"+this.minuteDay+ " | Night: "+ this.hourNight+"-"+this.minuteNight +" | Random: "+ this.hourRan+"-"+this.minuteRan);
          }
          else console.log("At random hour: "+new Date().getHours()+"-"+new Date().getMinutes()+" => Fail!!");
        }
      );


  }
  getRandomArbitrary(min:number, max:number) {
    return Math.random() * (max - min) + min;
  }
  isReady(hour:number, minute:number, sec:number){
    if(hour==this.hourDay && minute==this.minuteDay && sec==0) return 1;
    if(hour==this.hourNight && minute==this.minuteNight && sec==0) return 1;
    if(hour==this.hourRan && minute==this.minuteRan && sec==0) return 2;
    return 0;
  }
}
