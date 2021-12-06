import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  id:any;
  product:any;
  constructor(private $route: ActivatedRoute,public productService:ProductService) { }

  ngOnInit(): void {
    this.$route.params.forEach(param =>
      this.id = param['id']
    );
    this.productService.getProductById(this.id).subscribe((data: any) => {
      if (data) {
        this.product = data.product;
      } 
    });
  }

}
