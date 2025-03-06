import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { publicRoutes } from "@configs/index";
import { Error } from "@pages/index";
import { DefaultLayout } from "@layouts/index";

const AppRouter = () => {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <DefaultLayout />,
      errorElement: <Error />,
      hydrateFallbackElement: <>Hydrate Fallback Handing</>,
      children: publicRoutes.map((r) => {
        return {
          index: r.index,
          path: r.path,
          element: <r.component />,
          ...(r.validate && { loader: r.validate }),
        };
      }),
    },
  ]);
  return <RouterProvider router={router} />;
};

export default AppRouter;
