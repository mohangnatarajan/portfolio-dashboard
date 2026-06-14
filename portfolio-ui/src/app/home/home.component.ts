import { Component, OnInit, inject, signal } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
  imports: [],
  template: `
    <div class="flex min-h-screen items-center justify-center bg-gray-50">
      <div class="text-center">
        @if (loading()) {
          <p class="text-gray-400 text-lg">Loading...</p>
        } @else if (error()) {
          <p class="text-red-500 text-lg">{{ error() }}</p>
        } @else {
          <h1 class="text-5xl font-bold text-gray-800">{{ message() }}</h1>
          <p class="mt-4 text-gray-500">Response from .NET Core API</p>
        }
      </div>
    </div>
  `,
})
export class HomeComponent implements OnInit {
  private apiService = inject(ApiService);

  message = signal('');
  loading = signal(true);
  error = signal('');

  ngOnInit() {
    this.apiService.getHello().subscribe({
      next: (res) => {
        this.message.set(res.message);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(`Could not connect to API — is the .NET server running on ${this.apiService.baseUrl}?`);
        this.loading.set(false);
      },
    });
  }
}
