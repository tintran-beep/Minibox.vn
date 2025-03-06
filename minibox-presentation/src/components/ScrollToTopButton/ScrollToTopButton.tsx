import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowUp } from "@fortawesome/free-solid-svg-icons";

const ScrollToTopButton = () => {
    const [visible, setVisible] = useState(false);

    const toggleVisible = () => {
        const scrolled = document.documentElement.scrollTop;
        if (scrolled > 300) {
            setVisible(true);
        } else if (scrolled <= 300) {
            setVisible(false);
        }
    };

    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });
    };
    window.addEventListener("scroll", toggleVisible);

    return (
        <button className="fixed z-50 right-5 bottom-10 bg-primary p-3 cursor-pointer rounded-sm"  style={{ display: visible ? "inline" : "none" }} onClick={scrollToTop}>
            <FontAwesomeIcon icon={faArrowUp} />
        </button>
    );
}
 
export default ScrollToTopButton;