import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCartShopping
} from "@fortawesome/free-solid-svg-icons";

const ShoppingIcon = ({value}: {value:number} ) => {
    const formatvalue = value > 99 ? `99+` : value;

    return ( 
        <div className="p-2 cursor-pointer relative">
            <FontAwesomeIcon icon={faCartShopping} />
            <div className="size-5 rounded-full bg-primary absolute text-xs -top-1 -right-1"><div className="p-0.5 inset-0 size-full content-center">{formatvalue}</div></div>
        </div>
     );
}
 
export default ShoppingIcon;