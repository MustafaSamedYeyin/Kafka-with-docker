import { environment } from './../../../../environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Kafka {
  constructor(private httpClient : HttpClient)
  {
  }

  produceMessage(message: string) {
    return this.httpClient.post(`${environment.apiUrl}/kafka/produce?message=${message}`, {});
  }

  consumeMessage() {
    return this.httpClient.get(`${environment.apiUrl}/kafka/consume`);
  }
}