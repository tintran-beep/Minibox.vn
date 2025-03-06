import VideoPlaper from "@components/VideoSesion/VideoPlayer.jsx";
import { useNavigate } from "react-router-dom";
const VideoSesion = () => {
  const link = "https://docs.material-tailwind.com/demo.mp4";
  const navigate = useNavigate();

  return (
    <section className="mt-10 mb-10">
      <div className="w-full flex flex-col-reverse lg:flex-row bg-[#EDF1F0]">
        <div className="flex-1 self-center">
          <div className="py-auto px-12 lg:px-32 flex flex-col gap-6 mt-2 mb-2">
            <div className="text-xl font-semibold">Special Offer</div>
            <div className="text-3xl lg:text-4xl font-bold">
              Beauty Inspired by Real Life
            </div>
            <div className="text-md text-gray-500">
              Made using clean, non-toxic ingredients, our products are designed
              for everyone.
            </div>
            <div>
              <button className="bg-white font-semibold w-44 h-12 rounded-md cursor-pointer hover:bg-primary-brightness"
              onClick={()=>navigate('/')}>
                Discover Now
              </button>
            </div>
          </div>
        </div>
        <div className="flex-1 sm:px-2 h-[600px]">
          <VideoPlaper link={link} />
        </div>
      </div>
    </section>
  );
};

export default VideoSesion;
