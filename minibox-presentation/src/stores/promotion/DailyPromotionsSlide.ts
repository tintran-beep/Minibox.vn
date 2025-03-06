import { createSlice } from "@reduxjs/toolkit";
import actGetDailyPromotion from "./act/actGetDailyPromotion";
import { TLoading } from "@customtypes/shared";
import { TDailyPromotion } from "@customtypes/daily-promotion";

interface ITPromotionsState {
  records: TDailyPromotion[];
  loading: TLoading;
  error: string | null;
}

const initialState: ITPromotionsState = {
  records: [],
  loading: "idle",
  error: null,
};

const dailyPromotionsSlide = createSlice({
  name: "dailyPromotions",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(actGetDailyPromotion.pending, (state) => {
      state.loading = "pending";
      state.error = null;
    });
    builder.addCase(actGetDailyPromotion.fulfilled, (state, action) => {
      state.loading = "succeeded";
      state.records = action.payload;
    });
    builder.addCase(actGetDailyPromotion.rejected, (state, action) => {
      state.loading = "failed";
      if (action.payload && typeof action.payload === "string") {
        state.error = action.payload;
      }
    });
  },
});

export { actGetDailyPromotion };
export default dailyPromotionsSlide.reducer;