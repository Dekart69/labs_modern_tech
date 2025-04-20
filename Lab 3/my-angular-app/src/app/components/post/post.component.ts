import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpService } from '../../services/http-service';
import { Router } from '@angular/router';
import { WindowService } from '../../services/window-service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
  imports: [ReactiveFormsModule]
})
export class PostComponent {
  postForm: FormGroup;
  private readonly localStorage: Storage;

  constructor(
    private fb: FormBuilder,
    private httpService: HttpService,
    private router: Router,
    private readonly window: WindowService,
  ) {
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });
    this.localStorage = this.window.nativeWindow.localStorage;
  }

  onSubmit() {
    if (this.postForm.valid) {
      const { title, content } = this.postForm.value;
      const token = this.localStorage.getItem('token');
      if (token) {
        this.httpService.createPost(title, content, token).subscribe(id => {
          this.router.navigate(['/home']);
        });
      }
    }
  }
}
