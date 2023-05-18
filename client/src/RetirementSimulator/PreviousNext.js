import './PreviousNext.css';
import { useNavigate } from "react-router-dom";


// the func get props who set witch page to go
function PreviousNext(props) {
    const navigate = useNavigate();
    return (
        <div className="group-289375">
            {props.previous !== "null" ? (<button className="button1"
                onClick={() => {
                    navigate(`/${props.previous}`)
                }}>
                הקודם</button>) : (null)}

            <button className="button2" onClick={() => {
                navigate(`/${props.next}`)
            }}>
                {props.next === "calculats" ? "חשב" : "הבא"}</button>
        </div>
    );
}
export default PreviousNext;