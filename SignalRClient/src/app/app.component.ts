import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SignalRClient';
  constructor(public signalRService: SignalRService, private http: HttpClient) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.signalRService.addBroadcastChartDataListener();
    //this.startHttpRequest();
  }

  //test send message
  private startHttpRequest = () => {
    console.log('startHttpRequest');
    var mess = "Ok không?";
    this.http.get('https://localhost:44380/api/chat/send/message/' + mess)
      .subscribe(res => {
        console.log(res);
      })
  }
}
