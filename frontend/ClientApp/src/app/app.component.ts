import { RealtimeService } from './services/realtime.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(public realTimeService: RealtimeService) {
    this.realTimeService.startConnection();
    this.realTimeService.addNotificationListener();
  }
  
}
