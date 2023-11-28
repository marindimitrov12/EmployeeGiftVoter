import React from 'react'
export default function VoteRecord(props){
    return (<>
    <div className="col mb-5">
    <div className="card h-100">
                         
                         <img className="card-img-top" src={props.ImgUrl} alt="..." />
                        
                         <div className="card-body p-4">
                             <div className="text-center">
                               
                             <p className="fw-bolder">{`Name:${props.VoterName}`}</p>
                                 <h5 className="fw-bolder">{props.GiftVoted===null?`Didn't vote`:`Vote for:${props.GiftVoted}`}</h5>
                               </div>
                             
                         </div>
                         <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                           
                        
                         </div>
                   
                       
                     </div>
                     </div>
    </>)
}