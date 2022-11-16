import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { FeedbackDto } from '../model/feedbackDto.model';
import { FeedbackService } from '../services/feedback.service'

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  public dataSourceFeedbacks: FeedbackDto[] = [];
  public feedbacks: FeedbackDto[] = [];
  public feedbackCount = 0;

  constructor(private feedbackService: FeedbackService, private router: Router) { }

  ngOnInit(): void {
    this.feedbackCount = 1;
    this.feedbackService.getAllFeedbackPublicDtos().subscribe(res => {
      this.feedbacks = res;
      if (this.feedbackCount > this.feedbacks.length) {
        this.feedbackCount = this.feedbacks.length
      }
      this.dataSourceFeedbacks = this.feedbacks.slice(0, this.feedbackCount);
    })
  }

  showMore() {
    this.feedbackCount += 2;
    if (this.feedbackCount > this.feedbacks.length) {
      this.feedbackCount = this.feedbacks.length
    }
    this.dataSourceFeedbacks = this.feedbacks.slice(0, this.feedbackCount);
  }

  loginUser(){
    this.router.navigate(['/login']);
  }

  registerPatient(){
    this.router.navigate(['/register']);
  }
}