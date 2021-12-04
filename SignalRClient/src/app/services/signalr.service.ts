import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { MessageModal } from '../interfaces/mesage.model';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    public data!: MessageModal;
    public bradcastedData!: MessageModal;
    private hubConnection!: signalR.HubConnection;
    //private token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IntcInVzZXJfaWRcIjoyM30iLCJyb2xlIjoiQXV0aEh1YiIsIm5iZiI6MTYzNzc4NDg3NywiZXhwIjoxNjM3Nzg4NDc3LCJpYXQiOjE2Mzc3ODQ4Nzd9.o41PwuJm8zkK5DU_1LBv_tW06JQ6poYzm9rzlsin0wc";
    private token = "";
         
    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44380/chat', { accessTokenFactory: () => this.token })
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addTransferChartDataListener = () => {
        this.hubConnection.on('chat_request', (data) => {
            this.data = data;
            console.log(data);
        });
    }

    public broadcastChartData = () => {
        this.hubConnection.invoke('new_mess', this.data)
            .catch(err => console.error(err));
    }

    public addBroadcastChartDataListener = () => {
        this.hubConnection.on('new_mess', (data) => {
            this.bradcastedData = data;
            console.log(data);
        })
    }
}
