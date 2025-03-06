import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/swiper-bundle.css";
import { Navigation, Pagination, Autoplay } from "swiper/modules";
import { TGeneralPromotion } from "@customtypes/general-promotion";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@stores/hooks";
import { actGetGeneralPromotion } from "@stores/promotion/PromotionsSlide";

const GeneralPromotion = () => {
  const dispatch = useAppDispatch();

  const { records } = useAppSelector((state) => state.generalPromotions);

  useEffect(() => {
    if (!records.length) {
      dispatch(actGetGeneralPromotion());
    }
  }, [dispatch]);

  return (
    <>
      <section className="mt-18 mb-18">
        <div className="px-12 lg:px-36">
          <Swiper
            loop={true}
            autoplay={{
              delay: 3500,
              disableOnInteraction: false,
            }}
            slidesPerView={3}
            spaceBetween={3}
            pagination={{
              clickable: true,
            }}
            breakpoints={{
              640: {
                slidesPerView: 1,
                spaceBetween: 0,
              },
              768: {
                slidesPerView: 2,
                spaceBetween: 20,
              },
              1024: {
                slidesPerView: 3,
                spaceBetween: 25,
              },
            }}
            modules={[Autoplay, Pagination, Navigation]}
          >
            {records.length > 0 ? (
              records.map((p: TGeneralPromotion) => (
                <SwiperSlide key={p.id}>
                  <div className="relative group overflow-hidden w-full lg:h-150">
                    <img
                      className="object-cover h-full w-full transition-all duration-300 group-hover:scale-125"
                      src={p.img}
                      alt=""
                    />
                    <div className="content-end absolute inset-0">
                      <div className="flex flex-col gap-3 text-center py-8 sm:py-16 md:py-16 lg:py-16">
                        <div>
                          <h3 className="text-xl font-extrabold  text-white sm:text-3xl md:text-2xl lg:text-3xl">
                            {p.title}
                          </h3>
                        </div>
                        <div>
                          <a
                            href="/"
                            className=" text-sm text-white sm:text-lg md:text-xl lg:text-xl cursor-pointer "
                          >
                            {p.sub}
                          </a>
                        </div>
                      </div>
                    </div>
                  </div>
                </SwiperSlide>
              ))
            ) : (
              <></>
            )}
          </Swiper>
        </div>
      </section>
    </>
  );
};

export default GeneralPromotion;
