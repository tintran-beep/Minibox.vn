import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
    faBell
} from "@fortawesome/free-solid-svg-icons";

const NotifyIcon = ({value} : {value : number} ) => {
    return ( 
        <div className="p-2 cursor-pointer relative">
            <FontAwesomeIcon icon={faBell} />
            {
                value > 0 ? (<div className="size-5 rounded-full bg-primary absolute text-xs -top-1 -right-1"><div className="p-0.5 inset-0 size-full content-center">{value > 99 ? `99+` : value}</div></div>) : <></>
            }
        </div>
     );
}
 
export default NotifyIcon;