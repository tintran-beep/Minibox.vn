import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/swiper-bundle.css";
import { Navigation, Autoplay } from "swiper/modules";
import CountDownClock from "@components/CountDownClock/CountDownClock";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@stores/hooks";
import { actGetDailyPromotion } from "@stores/promotion/DailyPromotionsSlide";
import { TDailyPromotion } from "@customtypes/daily-promotion";
import { useNavigate } from "react-router-dom";
import { IDateStartCountDown } from "@interfaces/IDateStartCountDown";

const DailyPromotion = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { records } = useAppSelector((state) => state.dailyPromotions);

  useEffect(() => {
    if (!records.length) {
      dispatch(actGetDailyPromotion());
    }
  }, [dispatch]);

  const isDateAfterToday = (date : IDateStartCountDown) : boolean => {
    return new Date(new Date(date.years, date.month - 1, date.days,date.hours,date.minutes,date.miliseconds).toDateString()) > new Date(new Date().toDateString());
}

  return (
    <section className="mt-10 mb-10">
      <Swiper
        loop={true}
        autoplay={{
          delay: 3500,
          disableOnInteraction: false,
        }}
        slidesPerView={1}
        spaceBetween={3}
        pagination={{
          clickable: true,
        }}
        modules={[Autoplay, Navigation]}
      >
        {records.map((r: TDailyPromotion) => (
          isDateAfterToday(r.deadLine) ? <SwiperSlide key={r.id}>
            <div className="grid grid-cols-1 sm:grid-cols-2 px-8 lg:grid-cols-2 lg:ml-30 lg:gap-8">
              <div className="flex flex-col text-right overflow-hidden">
                <img
                  className="object-cover w-full h-[550px]"
                  src={r.img}
                  alt=""
                />
              </div>
              <div className="flex flex-col self-center gap-4 mt-4 sm:px-4">
                <div className="flex flex-row gap-2 text-lg font-medium">
                  <h4>{r.type}</h4>
                  <h4 className="bg-primary rounded-sm px-2 text-white">
                    -{r.discount}%
                  </h4>
                </div>
                <div className="text-2xl lg:text-4xl sm:text-3xl font-bold">
                  <h3>{r.title}</h3>
                </div>
                <div className="text-gray-500 text-lg font-medium">
                  <h5>{r.sub}</h5>
                </div>
                <div className="py-4">
                  <CountDownClock {...r.deadLine} />
                </div>
                <div className="">
                  <button
                    className="bg-black text-white py-3 px-16 rounded-sm cursor-pointer hover:bg-primary"
                    onClick={() => navigate("/")}
                  >
                    Get Info
                  </button>
                </div>
              </div>
            </div>
          </SwiperSlide>
        : <div key={r.id}></div>))}
      </Swiper>
    </section>
  );
};

export default DailyPromotion;
