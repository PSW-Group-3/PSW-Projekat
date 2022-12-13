import { Component, OnInit } from '@angular/core';
import { Feedback } from 'src/app/modules/hospital/model/feedback.model';
import { FeedbackService } from 'src/app/modules/hospital/services/feedback.service';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';


@Component({
  selector: 'app-create-feedback',
  templateUrl: './create-feedback.component.html',
  styleUrls: ['./create-feedback.component.css']
})
export class CreateFeedbackComponent{

  public feedback: Feedback = new Feedback();


  constructor(private feedbackService: FeedbackService, private router: Router) { }

  public createFeedback() {
    if (!this.isValidInput()) {
      alert("Data missing");
    };
    let id = localStorage.getItem("currentUserId");
    if(id!=null)
      this.feedback.userId = id;
    this.feedbackService.createFeedback(this.feedback).subscribe(res => {
      this.router.navigate(['']);
    });
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch(Error) {
      return null;
    }
  }

  private isValidInput(): boolean {
    return this.feedback?.description != '';
  }

}
