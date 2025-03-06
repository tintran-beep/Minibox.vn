import { Home, Categories, Products, Login, Register } from "@pages/index";

export const routesConstant: any = {
  home: "/",
  categories: "categories",
  productsByCateName: "products/category/:prefix",
  login: "login",
  register: "register",
  shop: "shop",
  blog: "blog",
};

/* don't need login */
export const publicRoutes: any[] = [
  { id: 1, path: routesConstant.login, component: Login, index: false },
  { id: 2, path: routesConstant.register, component: Register, index: false },
  { id: 3, path: routesConstant.home, component: Home, index: true },
  {
    id: 3,
    path: routesConstant.categories,
    component: Categories,
    index: false,
  },
  {
    id: 4,
    path: routesConstant.productsByCateName,
    component: Products,
    index: false,
    validate: ({ params }: { params: any }) => {
      if (
        typeof params.prefix !== "string" ||
        !/^[a-z]+$/i.test(params.prefix)
      ) {
        throw new Response("Bad Request", {
          statusText: "Category not found",
          status: 400,
        });
      }
      return true;
    },
  },
];

/* must login */
export const privateRoutes: any[] = [];
