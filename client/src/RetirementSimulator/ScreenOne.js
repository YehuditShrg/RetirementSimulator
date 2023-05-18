
import { useState } from "react";
import { useNavigate } from "react-router-dom";
// import AccrualPension from "./AccrualPension";
import AccrualPension from "./UserSee/AccrualPension";
// import BudgetPensin from "./BudgetPension";
import BudgetPensin from "./UserSee/BudgetPension";
import { BrowserRouter, Routes, Route } from 'react-router-dom';
// import Vacation from "./ScreenThree/Vacation";
import Vacation from "./UserSee/ScreenThree/Vacation";
import PreviousNext from "./PreviousNext";
import "./ScreenOne.css"
import Sequence from "./Sequence";

function ScreenOne() {
    const navigate = useNavigate();

    const [isBudgetPension, setIsBudget] = useState(false);
    const [isCollectiveAgrementOnly, setIsCollectiveAgreementOnly] = useState(true);

    const aaa = "pppp"
    return (
        <>
        <Sequence page="1"></Sequence>
            <div class="card bg-light mb-3">
                <div class="card-header">סוג פנסיה</div>
                <div class="card-body">
                    <p>איזה סוג פנסיה תרצה לחשב?</p>

                    <button className="btn btn-outline-primary" onClick={() => {
                        setIsBudget(true);
                    }}>פנסיה תקציבית</button>
                    <button name="accrualBtn" className="btn btn-outline-primary" >פנסיה צוברת</button>
                    <br></br>
                    <br></br>
                    {/* {isLogged === 0 ? (<Button title="Go to Login" onPress={() => navigation.navigate('Login')} > </Button>) : (<Button title="Stuff" onPress={() => navigation.navigate('DoStuff')} > </Button>)} */}
                    {isBudgetPension === true ? (<button className="btn btn-outline-primary">הסכם קיבוצי</button>) : (null)}
                    {isBudgetPension === true ? (<button className="btn btn-outline-primary" onClick={() => {
                        setIsCollectiveAgreementOnly(false);
                    }}> הסכם קיבוצי ושכר בכירים</button>) : (null)}

                    {/* <button onClick={() =>
                        { isBudgetPension === true && isCollectiveAgrementOnly === true ?
                         navigate(`/BudgetPensin`) : isBudgetPension === true ?
                          navigate(`/BudgetPensionForSeniorSalary`) : navigate(`/AccrualPension`) }}>הבא</button> */}
                    {/* <Vacation register={aaa}></Vacation> */}
                    {/* <button onClick={navigate(`/Vacation`)<Vacation></}>Vacation</button> */}

                </div>
            </div>
            <PreviousNext next="Vacation" previous="null"></PreviousNext>
        </>
    )
}
export default ScreenOne;