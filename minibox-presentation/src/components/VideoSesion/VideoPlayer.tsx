import { useEffect, useRef, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleXmark, faPlay } from "@fortawesome/free-solid-svg-icons";

let useClickOutside = (handler: () => void) => {
  let domNode  = useRef<HTMLDivElement>(null);

  useEffect(() => {
    let maybeHandler = (event : MouseEvent) => {
      if (
        domNode.current != undefined &&
        !domNode.current.contains(event.target as Node)
      ) {
        handler();
      }
    };
    document.addEventListener("mousedown", maybeHandler);

    return () => {
      document.removeEventListener("mousedown", maybeHandler);
    };
  });
  return domNode;
};

const VideoPlayer = ({ link }: { link: string }) => {
  const [videoOpen, setvideoOpen] = useState(false);

  let domNode = useClickOutside(() => {
    setvideoOpen(false);
  });

  return (
    <div ref={domNode} className="h-[300px] md:h-[350px] lg:h-full">
      <div className="relative overflow-hidden h-full">
        <div className="absolute left-0 top-0 h-full w-full">
          <video className="w-auto min-w-full min-h-full max-w-none" muted>
            <source
              src="https://docs.material-tailwind.com/demo.mp4"
              type="video/mp4"
            />
            Your browser does not support the video tag.
          </video>
        </div>
        <a
          onClick={() => setvideoOpen(true)}
          className="absolute inset-0 m-auto flex h-20 w-20 items-center justify-center rounded-full bg-white  md:h-[100px] md:w-[100px] cursor-pointer"
        >
          <span className="absolute right-0 top-0 z-[-1] h-full w-full animate-ping rounded-full bg-white bg-opacity-20 delay-500 duration-1000 transition-all"></span>
          <FontAwesomeIcon className=" text-black w-20 h-20" icon={faPlay} />
        </a>
      </div>

      {videoOpen && (
        <div className="fixed left-0 top-0 z-50 flex h-screen w-full items-center justify-center bg-black bg-opacity-70">
          <div className="mx-auto w-full max-w-[550px] bg-white">
            <>
              <iframe className="h-[320px] w-full" src={link} />
            </>
          </div>

          <button
            onClick={() => setvideoOpen(false)}
            className="absolute right-0 top-0 flex size-20 cursor-pointer items-center justify-center text-body-color hover:bg-black"
          >
            <FontAwesomeIcon className="text-white" icon={faCircleXmark} />
          </button>
        </div>
      )}
    </div>
  );
};

export default VideoPlayer;
