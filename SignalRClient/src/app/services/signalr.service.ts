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

    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44380/chat')
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
