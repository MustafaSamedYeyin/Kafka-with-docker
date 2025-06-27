import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Kafka } from './shared/kafka/kafka';

@Component({
  selector: 'app-root',
  imports: [FormsModule, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  standalone: true
})
export class App {
  protected title = 'messages';

  constructor(private kafkaService: Kafka) {

  }

  // Producer side
  producerMessage = '';
  producerMessages: { id: number; message: string; timestamp: Date }[] = [];

  // Consumer side
  consumerMessages: { id: number; message: string; timestamp: Date }[] = [];

  private messageId = 1;

  produceMessage() {
    var result = this.kafkaService.produceMessage(this.producerMessage).subscribe();
    this.consumeMessage();
  }

  consumeMessage() {
    this.kafkaService.consumeMessage().subscribe((message: any) => {
      this.consumerMessages.push({
        id: this.messageId++,
        message: message.value,
        timestamp: new Date()
      });
    });
  }
}