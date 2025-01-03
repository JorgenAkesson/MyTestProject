import { TestBed } from '@angular/core/testing';
import { BillingsService as BillingsService } from './billing-service.service';

describe('BillingService', () => {
  let service: BillingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BillingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
