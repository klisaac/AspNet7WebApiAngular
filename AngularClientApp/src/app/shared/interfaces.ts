export interface IProduct {
    productId: number;
    code:string;
    name: string;
    description: string;
    unitPrice: number;
    category: ICategory;
}

export interface IProductCreate {
    name: string;
    code:string;
    description: string;
    unitPrice: number;
    categoryId: number;
}

export interface IProductEdit {
    productId: number;
    name: string;
    code:string;
    description: string;
    unitPrice: number;
    categoryId: number;
}

export interface IProductGridItem {
    id: number;
    name: string;
    code: string;
    description: string;
    unitPrice: number;
    category: ICategory;
}

export interface ICategory {
    categoryId: number;
    name?: string;
    description?: string;
}

export interface ICategoryGridItem {
    id: number;
    name: string;
    description?: string;
}

export interface ICustomer {
    customerId: number;
    name: string;
    surName: string;
    address: string;
    city: string;
    state: string;
    email?: string;
    citizenId?: string;
}

export interface ICustomerOrder {
    customerId: number;
    customerName: string;
    customerSurName: string;
    orders: IOrder[];
}

export interface IOrder {
    orderId: number;
    grandTotal: number;
    orderItems: IOrderItem[];
}

export interface IOrderItem {
    orderItemId: number;
    productName: string;
    quantity:number;
    unitPrice:number;
    totalPrice: number;
}

export interface IPagedList<T> {
    pageIndex: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    items: T[];
}