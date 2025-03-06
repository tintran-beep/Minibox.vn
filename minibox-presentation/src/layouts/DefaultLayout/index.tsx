import WebSite from "./web";
import Mobile from "./mobile";
import { useViewport } from "@customhooks/index";

const DefaultLayout = () => {
  const viewPort = useViewport();
  const isMobile = viewPort.width <= 768;

  return(
    <div className="min-w-[300px] overflow-x-hidden">
        {
          isMobile ? (
            <Mobile/>
          ) : (
            <WebSite/>
          )
        }
    </div>
  ) 
};

export default DefaultLayout;
