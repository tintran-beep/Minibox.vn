import { configureStore } from "@reduxjs/toolkit";
import categories from "./category/categoriesSlice";
import products from "./product/productsSlice";
import generalPromotions from "./promotion/PromotionsSlide";
import dailyPromotions from "./promotion/DailyPromotionsSlide";
import brands from "./brands/brandsSlice";

export const store = configureStore({
  reducer: { categories, products, generalPromotions, dailyPromotions, brands },
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;

export default store;
