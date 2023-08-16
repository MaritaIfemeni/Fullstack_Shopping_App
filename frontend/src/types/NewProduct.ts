export interface NewProduct {
  id: string;
  productName: string;
  price: number;
  stock: number;
  description: string;
  productImages: {
    link: string;
  };
}
